(defn our-partial [f & args] (fn [& more-args] (apply f (concat args more-args))))
((our-partial * 1 2) 3)

(apply * '(1 2) )
(defn f [& a] (print a))
(f 1)
(apply * '(1 2) '(1 2))

(((partial juxt #(str %) first)) ["apple" "banana" "cherry"])
(str ['apple])
((comp (partial take 3) reverse) (range 1 10))
(vector [1 2])
(#(* % %) 3 1 1)
((comp #(vector % %) (juxt even? odd?)) 7)

(def a (partial * ((partial identity))))

(a 3)
((comp * (partial identity)) 1)

(defmacro square [a]  (* a a))
(square 4)

(str "a" "b")

(loop [a 2] (if (= a 1) (println "hello") (recur (dec a))))

(def my-square (comp (partial apply *) (juxt identity identity)))
(my-square 5)

(numerator (apply / (conj (range 1 10) 11)))

((partial range 3))
((conj (partial map (/ 3))  (partial range 3)) 3)

(comment 
  ;; destructuring (list set)
  ;; syms strs keys
  ;; body on next line
  constantly
  lazy-seq
  cons
  mapcat
  lazy-cat
  cycle
  map-indexed
  keep
  keep-indexed
  rand-nth)